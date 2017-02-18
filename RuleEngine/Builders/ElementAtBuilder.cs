using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using RuleEngine.Model;
using RuleEngine.Model.Locators;

namespace RuleEngine.Builders
{
    public class ElementAtBuilder : ExpressionBuilder
    {
        private readonly MethodInfo _elementAt =
            typeof(Enumerable).GetMethods()
                .Where(m => m.Name == "ElementAt")
                .Single(m => m.GetParameters().Length == 2);

        public override Expression BuildExpression(Locator locator, Expression parent, int level)
        {
            var elementAtLocator = (ElementAtLocator) locator;
            var result = MakeElementAtExpression(elementAtLocator.Index, parent);
            return result;
        }


        public Expression MakeElementAtExpression(int index, Expression prevCall)
        {
            var method = _elementAt.MakeGenericMethod(prevCall.Type.GetGenericArguments());
            var result = Expression.Call(null, method, prevCall, Expression.Constant(index));
            return result;
        }
    }
}