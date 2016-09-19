using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Visitor<TSource, TDestination> : ExpressionVisitor
    {
        public ParameterExpression ParameterExp { get; private set; }

        public Visitor(ParameterExpression parameterExp)
        {
            ParameterExp = parameterExp;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return ParameterExp;
        }
        protected override Expression VisitMember(MemberExpression node)
        {
            var expr = Visit(node.Expression);
            if (expr.Type != node.Expression.Type)
            {
                return Expression.MakeMemberAccess(expr, expr.Type.GetMember(node.Member.Name).Single());
            }
            return base.VisitMember(node);
        }

    }
}
