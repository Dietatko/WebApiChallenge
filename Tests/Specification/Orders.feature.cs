﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.4.0.0
//      SpecFlow Generator Version:2.4.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace CheckoutChallenge.AcceptanceTests.Specification
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.4.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class OrdersFeature : Xunit.IClassFixture<OrdersFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "Orders.feature"
#line hidden
        
        public OrdersFeature(OrdersFeature.FixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Orders", "\tAs a consumer,\r\n\tI want to manage orders,\r\n\tso that I can build an ordering func" +
                    "tionality", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.ScenarioTearDown();
        }
        
        [Xunit.FactAttribute(DisplayName="Lists no orders on start")]
        [Xunit.TraitAttribute("FeatureTitle", "Orders")]
        [Xunit.TraitAttribute("Description", "Lists no orders on start")]
        public virtual void ListsNoOrdersOnStart()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Lists no orders on start", null, ((string[])(null)));
#line 6
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 7
 testRunner.Given("I have running ordering service", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 8
  testRunner.Then("the service lists no existing orders", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Add new order")]
        [Xunit.TraitAttribute("FeatureTitle", "Orders")]
        [Xunit.TraitAttribute("Description", "Add new order")]
        public virtual void AddNewOrder()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add new order", null, ((string[])(null)));
#line 10
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 11
 testRunner.Given("I have running ordering service", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 12
  testRunner.When("I create new my order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 13
  testRunner.Then("the service lists the my order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 14
   testRunner.And("the service provides details about my order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Clearing an order")]
        [Xunit.TraitAttribute("FeatureTitle", "Orders")]
        [Xunit.TraitAttribute("Description", "Clearing an order")]
        public virtual void ClearingAnOrder()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Clearing an order", null, ((string[])(null)));
#line 16
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 17
 testRunner.Given("I have running ordering service", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 18
   testRunner.And("I created my order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 19
   testRunner.And("I added product E831967C-622E-4804-87B5-BDE90B37F5C4 to my order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 20
  testRunner.When("I clear my order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 21
  testRunner.Then("the service lists the my order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 22
   testRunner.And("the my order has no items", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Deleting a single order")]
        [Xunit.TraitAttribute("FeatureTitle", "Orders")]
        [Xunit.TraitAttribute("Description", "Deleting a single order")]
        public virtual void DeletingASingleOrder()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Deleting a single order", null, ((string[])(null)));
#line 24
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 25
 testRunner.Given("I have running ordering service", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 26
   testRunner.And("I created my order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 27
   testRunner.And("I added product E831967C-622E-4804-87B5-BDE90B37F5C4 to my order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 28
  testRunner.When("I delete my order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 29
  testRunner.Then("the service lists no existing orders", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Deleting one of the orders")]
        [Xunit.TraitAttribute("FeatureTitle", "Orders")]
        [Xunit.TraitAttribute("Description", "Deleting one of the orders")]
        public virtual void DeletingOneOfTheOrders()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Deleting one of the orders", null, ((string[])(null)));
#line 31
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 32
 testRunner.Given("I have running ordering service", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 33
   testRunner.And("I created first order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
   testRunner.And("I added product E831967C-622E-4804-87B5-BDE90B37F5C4 to first order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 35
   testRunner.And("I created second order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 36
   testRunner.And("I added product 0C0A5849-AF60-4EA7-A093-32B25A3D3E36 to second order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 37
  testRunner.When("I delete first order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 38
  testRunner.Then("the service lists 1 order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 39
  testRunner.Then("the service lists the second order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.4.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                OrdersFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                OrdersFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
