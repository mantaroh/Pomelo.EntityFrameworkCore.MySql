using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.TestModels.UpdatesModel;
using Xunit;

namespace Pomelo.EntityFrameworkCore.MySql.FunctionalTests
{
    public class UpdatesMySqlTest : UpdatesRelationalTestBase<UpdatesMySqlFixture>
    {
        public UpdatesMySqlTest(UpdatesMySqlFixture fixture)
            : base(fixture)
        {
        }

        public override void Identifiers_are_generated_correctly()
        {
            using (var context = CreateContext())
            {
                var entityType = context.Model.FindEntityType(typeof(
                    LoginEntityTypeWithAnExtremelyLongAndOverlyConvolutedNameThatIsUsedToVerifyThatTheStoreIdentifierGenerationLengthLimitIsWorkingCorrectly));
                Assert.Equal("LoginEntityTypeWithAnExtremelyLongAndOverlyConvolutedNameThatIs~", entityType.Relational().TableName);
                Assert.Equal("PK_LoginEntityTypeWithAnExtremelyLongAndOverlyConvolutedNameTha~", entityType.GetKeys().Single().Relational().Name);
                Assert.Equal("FK_LoginEntityTypeWithAnExtremelyLongAndOverlyConvolutedNameTha~", entityType.GetForeignKeys().Single().Relational().Name);
                Assert.Equal("IX_LoginEntityTypeWithAnExtremelyLongAndOverlyConvolutedNameTha~", entityType.GetIndexes().Single().Relational().Name);
            }
        }
    }
}
