using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using EF_Tutorial.Interface;
using EF_Tutorial.Models;
using EF_Tutorial.Controllers;
using EF_Tutorial.Data;
using Microsoft.AspNetCore.Mvc;

namespace BackendUnitTesting
{
    public class JsonDataTestController
    {
        private readonly Mock<IJsondata> _jsonInterface;

        public JsonDataTestController()
        {
            _jsonInterface = new Mock<IJsondata>();
        }


        [Fact]
        public void GetJsonData_JsonList() 
        {
            var jsonList = JsonDataList();
            _jsonInterface.Setup(x => x.GetJsonData())
                .Returns(jsonList);

            var jsonController = new JsonDataController(_jsonInterface.Object);

            var jsonResult = jsonController.GetJsonData();
            var okResult = jsonResult as OkObjectResult;

            Assert.NotNull(jsonResult);
            Assert.NotNull(okResult);
        }

        [Fact]
        public void GetJsonDataById_JsonList() 
        {
            var jsonList = JsonDataList();
            _jsonInterface.Setup(x => x.GetJsonDataById(1))
                .Returns(jsonList);

            var jsonController = new JsonDataController(_jsonInterface.Object);

            var jsonResult = jsonController.GetJsonDataById(1);
            var okResult = jsonResult as OkObjectResult;

            Assert.NotNull(jsonResult);
            Assert.NotNull(okResult);
        }

        private List<JsonData> JsonDataList()
        {
            List<JsonData> jsonData = new List<JsonData>
            {
                new JsonData 
                {
                    id = 1,
                    jsonId = 1,
                    date = "2022-01-01",
                    value1 = 102,
                    value2 = 125
                }
            };
            return jsonData;
        }
    }
}