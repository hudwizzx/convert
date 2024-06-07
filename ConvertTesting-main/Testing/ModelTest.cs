using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValuteConverter;

namespace Testing
{
    [TestClass]
    public class ModelTests
    {

        [TestMethod]
        public void CoinYepParser_GetValuteEntries_Should_Return_Correct_List()
        {
            // Arrange
            var dataTable = new DataTable();
            dataTable.Columns.Add("Vname");
            dataTable.Columns.Add("Vchcode");
            dataTable.Columns.Add("Vcurs");
            dataTable.Rows.Add("Доллар США", "USD", 99.09);

            // Act
            var valuteEntries = CoinYepParser.GetValuteEntries(dataTable);

            // Assert
            Assert.IsNotNull(valuteEntries);
            Assert.AreEqual(2, valuteEntries.Count());
        }

        [TestMethod]
        public void InputHandler_Unify_Should_Return_Correct_Unified_String()
        {
            // Arrange
            string input = "25.50";

            // Act
            var unifiedInput = InputHandler.Unify(input);

            // Assert
            Assert.AreEqual("25,50", unifiedInput);
        }
    }
}
