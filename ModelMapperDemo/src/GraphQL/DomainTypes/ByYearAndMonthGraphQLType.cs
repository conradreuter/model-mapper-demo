using GraphQL.Language.AST;
using GraphQL.Types;
using ModelMapperDemo.Model.DomainTypes;
using ModelMapperDemo.Utility;

namespace ModelMapperDemo.GraphQL.DomainTypes
{
    /// <summary>
    /// The GraphQL representation of a value by month and year.
    /// </summary>
    public class ByYearAndMonthGraphQLType : ScalarGraphType
    {
        public ByYearAndMonthGraphQLType()
        {
            Name = "ByYearAndMonth";
            Description = XmlDoc.ReadSummary(typeof(ByYearAndMonth<>));
        }

        public override object ParseLiteral(IValue value)
        {
            throw new System.NotImplementedException();
        }

        public override object ParseValue(object value) => value; // handled in mapper

        public override object Serialize(object value) => value; // handled in mapper
    }
}
