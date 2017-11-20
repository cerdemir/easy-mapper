using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace EasyMapper.test
{
    [TestClass]
    public class EasyMapperTest
    {

        [TestMethod]
        public void SetItemFromRow_Test()
        {

        }

        [TestMethod]
        public void SetItemToRow_Test()
        {

        }

        [TestMethod]
        public void CreateItemFromRow_Test()
        {
        }

        [TestMethod]
        public void CreateListFromTable_Test()
        {
        }

        [TestMethod]
        public void CreateDataTable_Test()
        {
            var mapper = new EasyMapper();
            var testData = GetTestEmployees();
            var datatable = mapper.CreateDataTable(testData);

            Assert.IsTrue(datatable.Columns.Contains("Id"));
        }

        private List<Employee> GetTestEmployees()
        {
            var testData = new Employee[] {
                new Employee {
                    Id =1,
                    Married = true,
                    Name = "Cemal",
                    Surname = "Erdemir",
                    Salary = 100
                },
                new Employee {
                    Id =2,
                    Married = false,
                    Name = "İrfan",
                    Surname = "Sağır",
                    Salary = 300
                }
            };
            return testData.ToList();
        }
    }
}
