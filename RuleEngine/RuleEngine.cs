using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using RuleEngine.Model;

namespace RuleEngine
{
	public class CompiledRule<TObject, TResult>
	{
		public string Name { get; set; }
		public Func<TObject, TResult> Process { get; set; }
	}

	public class RuleEngine<TObject> where TObject: class
    {
        private readonly ExpressionFactory _expressionFactory;

        public RuleEngine()
        {
            _expressionFactory = new ExpressionFactory();
        }

        public Func<TObject, bool> CompileRule<TRule>(TRule rule) where TRule : IRule
        {
            var paramUser = Expression.Parameter(typeof(TObject), typeof(TObject).Name.ToLower());

            var expr = GetExpressionForRule(rule, paramUser);

            var resultExpression = Expression.Lambda<Func<TObject, bool>>(expr, paramUser);
            return resultExpression.Compile();
        }

        public Func<TObject, TResult> CompileRule<TRule,TResult>(TRule rule, Func<TRule, TResult> result) where TRule : IRule
        {
            var paramUser = Expression.Parameter(typeof(TObject), typeof(TObject).Name.ToLower());
            var expr = GetExpressionForRule(rule, paramUser);
            var resultExpression = Expression.Lambda<Func<TObject, bool>>(expr, paramUser).Compile();
            Func<TObject, TResult> resultFunc = o => resultExpression(o) ? result(rule) : default(TResult);
            Expression<Func<TObject, TResult>> resultExpr = o => resultFunc(o);
            return resultExpr.Compile();
        }

        public IList<CompiledRule<TObject, TResult>> CompileRules<TRule, TResult>(IList<TRule> rules, Func<TRule, TResult> result)
            where TRule : IRule
        {
	        return rules.Select(r => new CompiledRule<TObject, TResult>
	        {
		        Name = r.Name,
				Process = CompileRule(r, result)
	        }).ToList();
        }

        public Action<TObject, Action<TObject, TRule>> CompileActionRule<TRule>(TRule rule) where TRule : IRule
        {
            var ruleResult = CompileRule(rule);

            Action<TObject, Action<TObject, TRule>> resultAction = (arg1, action) =>
            {
                if (ruleResult(arg1))
                {
                    action(arg1, rule);
                }
            };
            Expression<Action<TObject, Action<TObject, TRule>>> resultExpression = (arg1, action) => resultAction(arg1, action);
            return resultExpression.Compile();
        }

        public IList<Action<TObject, Action<TObject, TRule>>> CompileActionRules<TRule>(IList<TRule> rules) where TRule : IRule
        {
            return rules.Select(CompileActionRule).ToList();
        }

        public Expression<Func<TObject, bool>> GetExpressionRule(IRule rule)
        {
            var paramUser = Expression.Parameter(typeof(TObject), typeof(TObject).Name.ToLower());

            var expr = GetExpressionForRule(rule, paramUser);

            var resultExpression = Expression.Lambda<Func<TObject, bool>>(expr, paramUser);
            return resultExpression;
        }

        public IEnumerable<Func<TObject, bool>> CompileRules<TRule>(IList<TRule> rules) where TRule : IRule
        {
            return rules.Select(CompileRule);
        }

        private Expression GetExpressionForRule<TRule>(TRule rule, ParameterExpression paramUser) where TRule : IRule
        {
            var expressions = _expressionFactory.BuildExpressionForConditions(rule.Conditions, paramUser, 0);
            return expressions;
        }
    }
}