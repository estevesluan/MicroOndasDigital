using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MO;
using MO.Controllers;

namespace MO.Tests.Controllers
{
    [TestClass]
    public class MicroOndasController
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            MicroOndasController controller = new MicroOndasController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
