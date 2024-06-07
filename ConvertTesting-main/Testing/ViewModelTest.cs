using Moq;
using System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValuteConverter.Model.Interfaces;
using ValuteConverter;
using Moq;

namespace Testing
{
    [TestClass]
    public class ViewModelTest
    {
        [TestMethod]
        public async Task LoadValutes_Should_Load_ValuteEntries_From_CoinYepParser()
        {
            // Arrange
            var mockConverter = new Moq.Mock<ICourseConverter>();
            var viewModel = new ViewModel(mockConverter.Object);

            var dataTable = new System.Data.DataTable();
            dataTable.Columns.Add("Vname");
            dataTable.Columns.Add("Vchcode");
            dataTable.Columns.Add("Vcurs");
            dataTable.Rows.Add("Доллар США", "USD", 99.09);

            mockConverter.Setup(c => c.GetExchangeRateOnDateAsync(It.IsAny<DateTime>())).ReturnsAsync(dataTable);

            // Act
            await viewModel.LoadValutes();

            // Assert
            Assert.IsNotNull(viewModel.ValuteEntries);
            Assert.AreEqual(2, viewModel.ValuteEntries.Count);

        }

        [TestMethod]
        public void ConvertLeftToRight_Should_Convert_Left_To_Right_Text()
        {
            // Arrange
            var mockConverter = new Moq.Mock<ICourseConverter>();
            var viewModel = new ViewModel(mockConverter.Object);

            var rubValute = new ValuteEntry("Российский рубль", "RUB", 1.0);
            var usdValute = new ValuteEntry("Доллар США", "USD", 99.09);
            viewModel.SelectedValuteLeft = rubValute;
            viewModel.SelectedValuteRight = usdValute;
            viewModel.LeftText = "9909";

            // Act
            viewModel.ConvertLeftToRight();

            // Assert
            Assert.AreEqual("100", viewModel.RightText); 
        }
    }
}
