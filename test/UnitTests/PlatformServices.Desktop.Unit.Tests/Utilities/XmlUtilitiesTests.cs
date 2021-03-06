﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace MSTestAdapter.PlatformServices.Desktop.UnitTests.Utilities
{
    extern alias FrameworkV1;

    using System.IO;
    using System.Reflection;
    using System.Xml;
    using static AppDomainUtilitiesTests;

    using CollectionAssert = FrameworkV1::Microsoft.VisualStudio.TestTools.UnitTesting.CollectionAssert;
    using TestClass = FrameworkV1::Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute;
    using TestInitialize = FrameworkV1::Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute;
    using TestMethod = FrameworkV1::Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute;

    [TestClass]
    public class XmlUtilitiesTests
    {
        private TestableXmlUtilities testableXmlUtilities;

        [TestInitialize]
        public void TestInit()
        {
            this.testableXmlUtilities = new TestableXmlUtilities();
        }

        [TestMethod]
        public void AddAssemblyRedirectionShouldAddRedirectionToAnEmptyXml()
        {
            this.testableXmlUtilities.ConfigXml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>";
            var assemblyName = Assembly.GetExecutingAssembly().GetName();

            var configBytes = this.testableXmlUtilities.AddAssemblyRedirection(
                "foo.xml",
                assemblyName,
                "99.99.99.99",
                assemblyName.Version.ToString());

            // Assert.
            var expectedXml =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?><configuration><runtime><assemblyBinding xmlns=\"urn:schemas-microsoft-com:asm.v1\"><dependentAssembly><assemblyIdentity name=\"MSTestAdapter.PlatformServices.Desktop.UnitTests\" publicKeyToken=\"b03f5f7f11d50a3a\" culture=\"neutral\" /><bindingRedirect oldVersion=\"99.99.99.99\" newVersion=\"14.0.0.0\" /></dependentAssembly></assemblyBinding></runtime></configuration>";
            var doc = new XmlDocument();
            doc.LoadXml(expectedXml);
            byte[] expectedConfigBytes = null;

            using (var ms = new MemoryStream())
            {
                doc.Save(ms);
                expectedConfigBytes = ms.ToArray();
            }

            CollectionAssert.AreEqual(expectedConfigBytes, configBytes);
        }

        [TestMethod]
        public void AddAssemblyRedirectionShouldAddRedirectionToAnEmptyConfig()
        {
            this.testableXmlUtilities.ConfigXml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<configuration>
</configuration>";
            var assemblyName = Assembly.GetExecutingAssembly().GetName();

            var configBytes = this.testableXmlUtilities.AddAssemblyRedirection(
                "foo.xml",
                assemblyName,
                "99.99.99.99",
                assemblyName.Version.ToString());

            // Assert.
            var expectedXml =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?><configuration><runtime><assemblyBinding xmlns=\"urn:schemas-microsoft-com:asm.v1\"><dependentAssembly><assemblyIdentity name=\"MSTestAdapter.PlatformServices.Desktop.UnitTests\" publicKeyToken=\"b03f5f7f11d50a3a\" culture=\"neutral\" /><bindingRedirect oldVersion=\"99.99.99.99\" newVersion=\"14.0.0.0\" /></dependentAssembly></assemblyBinding></runtime></configuration>";
            var doc = new XmlDocument();
            doc.LoadXml(expectedXml);
            byte[] expectedConfigBytes = null;

            using (var ms = new MemoryStream())
            {
                doc.Save(ms);
                expectedConfigBytes = ms.ToArray();
            }

            CollectionAssert.AreEqual(expectedConfigBytes, configBytes);
        }

        [TestMethod]
        public void AddAssemblyRedirectionShouldAddRedirectionToAConfigWithARuntimeSectionOnly()
        {
            this.testableXmlUtilities.ConfigXml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<configuration>
<runtime>
</runtime>
</configuration>";
            var assemblyName = Assembly.GetExecutingAssembly().GetName();

            var configBytes = this.testableXmlUtilities.AddAssemblyRedirection(
                "foo.xml",
                assemblyName,
                "99.99.99.99",
                assemblyName.Version.ToString());

            // Assert.
            var expectedXml =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?><configuration><runtime><assemblyBinding xmlns=\"urn:schemas-microsoft-com:asm.v1\"><dependentAssembly><assemblyIdentity name=\"MSTestAdapter.PlatformServices.Desktop.UnitTests\" publicKeyToken=\"b03f5f7f11d50a3a\" culture=\"neutral\" /><bindingRedirect oldVersion=\"99.99.99.99\" newVersion=\"14.0.0.0\" /></dependentAssembly></assemblyBinding></runtime></configuration>";
            var doc = new XmlDocument();
            doc.LoadXml(expectedXml);
            byte[] expectedConfigBytes = null;

            using (var ms = new MemoryStream())
            {
                doc.Save(ms);
                expectedConfigBytes = ms.ToArray();
            }

            CollectionAssert.AreEqual(expectedConfigBytes, configBytes);
        }

        [TestMethod]
        public void AddAssemblyRedirectionShouldAddRedirectionToAConfigWithRedirections()
        {
            this.testableXmlUtilities.ConfigXml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><configuration><runtime><assemblyBinding xmlns=\"urn:schemas-microsoft-com:asm.v1\"><dependentAssembly><assemblyIdentity name=\"Random.UnitTests\" publicKeyToken=\"b03f5f7f11d50a3a\" culture=\"neutral\" /><bindingRedirect oldVersion=\"99.99.99.99\" newVersion=\"14.0.0.0\" /></dependentAssembly></assemblyBinding></runtime></configuration>";
            var assemblyName = Assembly.GetExecutingAssembly().GetName();

            var configBytes = this.testableXmlUtilities.AddAssemblyRedirection(
                "foo.xml",
                assemblyName,
                "99.99.99.99",
                assemblyName.Version.ToString());

            // Assert.
            var expectedXml =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?><configuration><runtime><assemblyBinding xmlns=\"urn:schemas-microsoft-com:asm.v1\"><dependentAssembly><assemblyIdentity name=\"Random.UnitTests\" publicKeyToken=\"b03f5f7f11d50a3a\" culture=\"neutral\" /><bindingRedirect oldVersion=\"99.99.99.99\" newVersion=\"14.0.0.0\" /></dependentAssembly><dependentAssembly><assemblyIdentity name=\"MSTestAdapter.PlatformServices.Desktop.UnitTests\" publicKeyToken=\"b03f5f7f11d50a3a\" culture=\"neutral\" /><bindingRedirect oldVersion=\"99.99.99.99\" newVersion=\"14.0.0.0\" /></dependentAssembly></assemblyBinding></runtime></configuration>";
            var doc = new XmlDocument();
            doc.LoadXml(expectedXml);
            byte[] expectedConfigBytes = null;

            using (var ms = new MemoryStream())
            {
                doc.Save(ms);
                expectedConfigBytes = ms.ToArray();
            }

            CollectionAssert.AreEqual(expectedConfigBytes, configBytes);
        }
    }
}
