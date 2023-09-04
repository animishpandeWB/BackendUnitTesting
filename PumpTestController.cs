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
    public class PumpTestController
    {
        private readonly Mock<IPump> _pumpInterface;

        public PumpTestController()
        {
            _pumpInterface = new Mock<IPump>();
        }

        [Fact]
        public void GetPumps_PumpList()
        {
            var pumpList = PumpDataList();
            _pumpInterface.Setup(x => x.GetPumps())
                .Returns(pumpList);

            var pumpController = new PumpController(_pumpInterface.Object);

            var pumpResult = pumpController.GetPumps();
            var okResult = pumpResult as OkObjectResult;

            Assert.NotNull(pumpResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(pumpList, okResult.Value);
        }

        [Fact]
        public void GetPumpByUserId_PumpList()
        {
            var pumpList = PumpDataList();
            _pumpInterface.Setup(x => x.GetPumpByUserId(1))
                .Returns(pumpList);

            var pumpController = new PumpController(_pumpInterface.Object);

            var pumpResult = pumpController.GetPumpsByUserId(1);
            var okResult = pumpResult as OkObjectResult;

            Assert.NotNull(pumpResult);
            Assert.NotNull(okResult.Value);
            Assert.Equal(pumpList, okResult.Value);
        }

        [Fact]
        public void GetPumpByPumpId_PumpList()
        {
            var pumpList = PumpDataList();
            _pumpInterface.Setup(x => x.GetPumpByPumpId(1))
                .Returns(pumpList[0]);

            var pumpController = new PumpController(_pumpInterface.Object);

            var pumpResult = pumpController.GetPumpsByPumpId(1);
            var okResult = pumpResult as OkObjectResult;

            Assert.NotNull(pumpResult);
            Assert.NotNull(okResult.Value);
            // Assert.Equal(pumpList, okResult.Value);
        }

        [Fact]
        public void AddPump_Pump()
        {
            var pumpList = PumpDataList();
            _pumpInterface.Setup(x => x.AddPump(pumpList[0]))
                .Returns(true);

            var pumpController = new PumpController(_pumpInterface.Object);

            //act
            var addPumpResult = pumpController.AddPump(pumpList[0]);
            var okResult = addPumpResult as OkObjectResult;
            //assert
            Assert.NotNull(addPumpResult);
            Assert.NotNull(okResult);
        }

        [Fact]
        public void DeletePump_Pump()
        {
            var pumpList = PumpDataList();
            _pumpInterface.Setup(x => x.DeletePump(1))
                .Returns(true);

            var pumpController = new PumpController(_pumpInterface.Object);

            var pumpResult = pumpController.DeletePump(1);
            var okResult = pumpResult as OkObjectResult;

            Assert.NotNull(pumpResult);
            Assert.NotNull(okResult.Value);
            // Assert.Equal(pumpList, okResult.Value);
        }
        


        private List<Pump> PumpDataList()
        {
            List<Pump> pumpData = new List<Pump>
            {
                new Pump
                {
                    Id = 1,
                    PumpId = 1,
                    Name = "Pump 1",
                    Type = "Centrifugal Pump",
                    Status = false,
                    UserId = 1,
                    User = null
                },
                new Pump
                {
                    Id = 2,
                    PumpId = 2,
                    Name = "Pump 2",
                    Type = "Jet Pump",
                    Status = true,
                    UserId = 1
                },
                new Pump
                {
                    Id = 3,
                    PumpId = 3,
                    Name = "Pump 3",
                    Type = "Piston Pump",
                    Status = false,
                    UserId = 1
                }
            };
            return pumpData;
        }
    }
}