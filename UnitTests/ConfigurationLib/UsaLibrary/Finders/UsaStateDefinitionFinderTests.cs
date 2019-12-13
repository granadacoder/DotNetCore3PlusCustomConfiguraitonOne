namespace MyCompany.MyExamples.CustomConfigurationExample.UnitTests.ConfigurationLib.UsaLibrary.Finders
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using MyCompany.MyExamples.CustomConfiguration.ConfigurationLib.UsaLibrary;
    using MyCompany.MyExamples.CustomConfiguration.ConfigurationLib.UsaLibrary.Finders;
    using MyCompany.MyExamples.CustomConfiguration.ConfigurationLib.UsaLibrary.Finders.Interfaces;
    using MyCompany.MyExamples.CustomConfiguration.ConfigurationLib.UsaLibrary.Retrievers.Interfaces;

    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class UsaStateDefinitionFinderTests
    {
        private const int NorthCarolinaUsaStateDefinitionUniqueIdentifier = 444;
        private const string NorthCarolinaUsaStateAbbreviation = "NC";
        private const string NorthCarolinaUsaStateFullName = "NorthCarolina";
        private const int VirginiaUsaStateDefinitionUniqueIdentifier = 333;
        private const string VirginiaUsaStateAbbreviation = "VA";
        private const string VirginiaUsaStateFullName = "Virginia";
        private const string DummyValue = "DummyValue";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullIUsaStateDefinitionConfigurationRetrieverConstructorTest()
        {
            IUsaStateDefinitionFinder testItem = new UsaStateDefinitionFinder(null);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void DuplicateByIdentifierTest()
        {
            UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter configurationWrapper = new UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter();
            configurationWrapper.UsaStateDefinitions.Add(this.GetNorthCarolina());
            UsaStateObject testTriggerItem = this.GetVirginia();
            testTriggerItem.UsaStateDefinitionUniqueIdentifier = NorthCarolinaUsaStateDefinitionUniqueIdentifier;
            configurationWrapper.UsaStateDefinitions.Add(testTriggerItem);
            configurationWrapper.UsaStateDefinitions.Add(testTriggerItem);

            Mock<IUsaStateDefinitionConfigurationRetriever> mockRetriever = this.GetDefaultIUsaStateDefinitionConfigurationRetrieverMock();
            mockRetriever.Setup(x => x.GetUsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter()).Returns(configurationWrapper);
            IUsaStateDefinitionFinder testItem = new UsaStateDefinitionFinder(mockRetriever.Object);
            testItem.FindUsaStateObjectByUniqueId(NorthCarolinaUsaStateDefinitionUniqueIdentifier);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void DuplicateByFullNameTest()
        {
            UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter configurationWrapper = new UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter();
            configurationWrapper.UsaStateDefinitions.Add(this.GetNorthCarolina());
            UsaStateObject testTriggerItem = this.GetVirginia();
            testTriggerItem.UsaStateFullName = NorthCarolinaUsaStateFullName;
            configurationWrapper.UsaStateDefinitions.Add(testTriggerItem);
            configurationWrapper.UsaStateDefinitions.Add(testTriggerItem);

            Mock<IUsaStateDefinitionConfigurationRetriever> mockRetriever = this.GetDefaultIUsaStateDefinitionConfigurationRetrieverMock();
            mockRetriever.Setup(x => x.GetUsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter()).Returns(configurationWrapper);
            IUsaStateDefinitionFinder testItem = new UsaStateDefinitionFinder(mockRetriever.Object);
            testItem.FindUsaStateObject(NorthCarolinaUsaStateFullName);
        }

        [TestMethod]
        public void UsaStateDefinitionsIsNullByFullNameTest()
        {
            UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter configurationWrapper = new UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter();
            configurationWrapper.UsaStateDefinitions = null;

            Mock<IUsaStateDefinitionConfigurationRetriever> mockRetriever = this.GetDefaultIUsaStateDefinitionConfigurationRetrieverMock();
            mockRetriever.Setup(x => x.GetUsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter()).Returns(configurationWrapper);
            IUsaStateDefinitionFinder testItem = new UsaStateDefinitionFinder(mockRetriever.Object);
            UsaStateObject foundItem = testItem.FindUsaStateObject(DummyValue);
            Assert.IsNull(foundItem);
        }

        [TestMethod]
        public void UsaStateDefinitionsIsNullByUniqueIdentifierTest()
        {
            UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter configurationWrapper = new UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter();
            configurationWrapper.UsaStateDefinitions = null;

            Mock<IUsaStateDefinitionConfigurationRetriever> mockRetriever = this.GetDefaultIUsaStateDefinitionConfigurationRetrieverMock();
            mockRetriever.Setup(x => x.GetUsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter()).Returns(configurationWrapper);
            IUsaStateDefinitionFinder testItem = new UsaStateDefinitionFinder(mockRetriever.Object);
            UsaStateObject foundItem = testItem.FindUsaStateObjectByUniqueId(0);
            Assert.IsNull(foundItem);
        }

        [TestMethod]
        public void FindByFullStateName()
        {
            IUsaStateDefinitionFinder testItem = new UsaStateDefinitionFinder(this.GetDefaultIUsaStateDefinitionConfigurationRetrieverMock().Object);
            UsaStateObject foundItem = testItem.FindUsaStateObject(VirginiaUsaStateFullName);
            Assert.IsNotNull(foundItem);
            Assert.AreEqual(VirginiaUsaStateDefinitionUniqueIdentifier, foundItem.UsaStateDefinitionUniqueIdentifier);
            Assert.AreEqual(VirginiaUsaStateAbbreviation, foundItem.UsaStateAbbreviation);
            Assert.AreEqual(VirginiaUsaStateFullName, foundItem.UsaStateFullName);
        }

        [TestMethod]
        public void FindByUniqueIdentifier()
        {
            IUsaStateDefinitionFinder testItem = new UsaStateDefinitionFinder(this.GetDefaultIUsaStateDefinitionConfigurationRetrieverMock().Object);
            UsaStateObject foundItem = testItem.FindUsaStateObjectByUniqueId(VirginiaUsaStateDefinitionUniqueIdentifier);
            Assert.IsNotNull(foundItem);
            Assert.AreEqual(VirginiaUsaStateDefinitionUniqueIdentifier, foundItem.UsaStateDefinitionUniqueIdentifier);
            Assert.AreEqual(VirginiaUsaStateAbbreviation, foundItem.UsaStateAbbreviation);
            Assert.AreEqual(VirginiaUsaStateFullName, foundItem.UsaStateFullName);
        }

        private Mock<IUsaStateDefinitionConfigurationRetriever> GetDefaultIUsaStateDefinitionConfigurationRetrieverMock()
        {
            Mock<IUsaStateDefinitionConfigurationRetriever> returnMock = new Mock<IUsaStateDefinitionConfigurationRetriever>();
            returnMock.Setup(x => x.GetUsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter()).Returns(this.GetDefaultUsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter());
            return returnMock;
        }

        private UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter GetDefaultUsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter()
        {
            UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter returnItem = new UsaStateConfigurationWrapperThisObjectNameDoesNotReallyMatter();
            returnItem.UsaStateDefinitions.Add(this.GetNorthCarolina());
            returnItem.UsaStateDefinitions.Add(this.GetVirginia());
            return returnItem;
        }

        private UsaStateObject GetNorthCarolina()
        {
            UsaStateObject returnItem = new UsaStateObject();
            returnItem.UsaStateDefinitionUniqueIdentifier = NorthCarolinaUsaStateDefinitionUniqueIdentifier;
            returnItem.UsaStateAbbreviation = NorthCarolinaUsaStateAbbreviation;
            returnItem.UsaStateFullName = NorthCarolinaUsaStateFullName;

            return returnItem;
        }

        private UsaStateObject GetVirginia()
        {
            UsaStateObject returnItem = new UsaStateObject();
            returnItem.UsaStateDefinitionUniqueIdentifier = VirginiaUsaStateDefinitionUniqueIdentifier;
            returnItem.UsaStateAbbreviation = VirginiaUsaStateAbbreviation;
            returnItem.UsaStateFullName = VirginiaUsaStateFullName;

            return returnItem;
        }
    }
}
