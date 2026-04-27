using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WCMS.WebSystem.IntegrationTests
{
    /// <summary>
    /// Verifies that legacy API controller stubs return proper 410 Gone responses.
    /// These controllers live in separate WebApp projects (Integration, SystemPartsG2)
    /// so we test the controller action directly via reflection.
    /// </summary>
    [TestClass]
    public class LegacyEndpointCompatibilityTests
    {
        private static Assembly _integrationAssembly;
        private static Assembly _g2Assembly;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            // Navigate from test bin to sibling project bin directories
            // Test output: Tests/WCMS.WebSystem.IntegrationTests/bin/Debug/net10.0/
            // Integration: Portal/WebParts/Integration/IntegrationParts/bin/Debug/net10.0/
            // G2:          Portal/WebParts/SystemPartsG2/SystemPartsG2/bin/Debug/net10.0/
            var baseDir = AppContext.BaseDirectory;
            var solutionRoot = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "..", ".."));

            var integrationDll = Path.Combine(solutionRoot,
                "Portal", "WebParts", "Integration", "IntegrationParts",
                "bin", "Debug", "net10.0", "WCMS.WebSystem.Apps.Integration.WebApp.dll");
            var g2Dll = Path.Combine(solutionRoot,
                "Portal", "WebParts", "SystemPartsG2", "SystemPartsG2",
                "bin", "Debug", "net10.0", "WCMS.WebSystem.WebParts.SystemPartsG2.WebApp.dll");

            if (File.Exists(integrationDll))
                _integrationAssembly = Assembly.LoadFrom(integrationDll);
            if (File.Exists(g2Dll))
                _g2Assembly = Assembly.LoadFrom(g2Dll);
        }

        [TestMethod]
        [DataRow("WCMS.WebSystem.WebParts.Integration.MembervisitprintpreviewApiController", "MemberVisitPrintPreview.ashx", true)]
        [DataRow("WCMS.WebSystem.WebParts.Integration.AsopWsApiController", "ASOP-WS.asmx", true)]
        [DataRow("WCMS.WebSystem.WebParts.Integration.PlaybackApiController", "Playback.ashx", true)]
        [DataRow("WCMS.WebSystem.WebParts.Integration.MemberApiController", "Member.asmx", true)]
        [DataRow("WCMS.WebSystem.WebParts.Integration.DatasyncApiController", "DataSync.svc", true)]
        [DataRow("WCMS.WebSystem.WebParts.Integration.MemberserviceApiController", "MemberService.asmx", true)]
        [DataRow("WCMS.WebSystem.WebParts.Integration.VerifysessionApiController", "VerifySession.ashx", true)]
        [DataRow("WCMS.WebSystem.WebParts.Integration.MusicApiController", "Music.svc", true)]
        [DataRow("WCMS.WebSystem.WebParts.Integration.BibleserviceApiController", "BibleService.asmx", true)]
        [DataRow("WCMS.WebSystem.WebParts.Integration.ExternalApiController", "External.asmx", true)]
        [DataRow("WCMS.WebSystem.WebParts.Integration.MakeupApiController", "MakeUp.ashx", true)]
        [DataRow("WCMS.WebSystem.WebParts.G2.WebserviceApiController", "WebService.asmx", false)]
        [DataRow("WCMS.WebSystem.WebParts.G2.FlashserviceApiController", "FlashService.asmx", false)]
        [DataRow("WCMS.WebSystem.WebParts.G2.HandlerApiController", "Handler.ashx", false)]
        public void RetiredLegacyController_Returns410Gone(string typeName, string legacyName, bool isIntegration)
        {
            var assembly = isIntegration ? _integrationAssembly : _g2Assembly;
            if (assembly == null)
            {
                Assert.Inconclusive($"Assembly for {legacyName} not found in build output. Build the full solution first.");
                return;
            }

            var controllerType = assembly.GetType(typeName);
            Assert.IsNotNull(controllerType, $"Controller type {typeName} not found in assembly");

            var controller = (ControllerBase)Activator.CreateInstance(controllerType)!;
            var method = controllerType.GetMethod("Get")!;
            var result = (ObjectResult)method.Invoke(controller, null)!;

            Assert.AreEqual(410, result.StatusCode, $"Controller for {legacyName} should return 410");

            var json = JsonSerializer.Serialize(result.Value);
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            Assert.AreEqual("gone", root.GetProperty("status").GetString());
            Assert.AreEqual(legacyName, root.GetProperty("legacy").GetString());
            Assert.IsTrue(root.GetProperty("message").GetString()!.Contains("retired"),
                $"Response message for {legacyName} should indicate endpoint is retired");
        }
    }
}
