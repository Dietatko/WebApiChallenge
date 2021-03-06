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
    public partial class OrderItemsFeature : Xunit.IClassFixture<OrderItemsFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "OrderItems.feature"
#line hidden
        
        public OrderItemsFeature(OrderItemsFeature.FixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "OrderItems", "\tAs a consumer,\r\n\tI want to manage items in an order,\r\n\tso that I can build an or" +
                    "dering functionality", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        
        [Xunit.FactAttribute(DisplayName="Newly created order has no items")]
        [Xunit.TraitAttribute("FeatureTitle", "OrderItems")]
        [Xunit.TraitAttribute("Description", "Newly created order has no items")]
        public virtual void NewlyCreatedOrderHasNoItems()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Newly created order has no items", null, ((string[])(null)));
#line 6
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 7
 testRunner.Given("I have running ordering service", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 8
  testRunner.When("I create new my order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 9
  testRunner.Then("the my order should not have any items", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Add new item")]
        [Xunit.TraitAttribute("FeatureTitle", "OrderItems")]
        [Xunit.TraitAttribute("Description", "Add new item")]
        public virtual void AddNewItem()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add new item", null, ((string[])(null)));
#line 11
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 12
 testRunner.Given("I have running ordering service", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 13
   testRunner.And("I created my order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
  testRunner.When("I add product E831967C-622E-4804-87B5-BDE90B37F5C4 to my order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 15
  testRunner.Then("the my order should have 1 item", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 16
   testRunner.And("the my order should contain product E831967C-622E-4804-87B5-BDE90B37F5C4", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Update item amount")]
        [Xunit.TraitAttribute("FeatureTitle", "OrderItems")]
        [Xunit.TraitAttribute("Description", "Update item amount")]
        public virtual void UpdateItemAmount()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Update item amount", null, ((string[])(null)));
#line 18
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 19
 testRunner.Given("I have running ordering service", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 20
   testRunner.And("I created my order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 21
   testRunner.And("I added product E831967C-622E-4804-87B5-BDE90B37F5C4 with amount 2.5 to my order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
  testRunner.When("I update amount of product E831967C-622E-4804-87B5-BDE90B37F5C4 in my order to 4." +
                    "6", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 23
  testRunner.Then("the my order should have 1 item", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 24
   testRunner.And("the my order contains product E831967C-622E-4804-87B5-BDE90B37F5C4 with amount 4." +
                    "6", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Amount is summed if same product is added")]
        [Xunit.TraitAttribute("FeatureTitle", "OrderItems")]
        [Xunit.TraitAttribute("Description", "Amount is summed if same product is added")]
        public virtual void AmountIsSummedIfSameProductIsAdded()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Amount is summed if same product is added", null, ((string[])(null)));
#line 26
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 27
 testRunner.Given("I have running ordering service", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 28
   testRunner.And("I created my order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
  testRunner.When("I add product E831967C-622E-4804-87B5-BDE90B37F5C4 with amount 2.2 to my order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 30
   testRunner.And("I add product E831967C-622E-4804-87B5-BDE90B37F5C4 with amount 5.5 to my order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 31
  testRunner.Then("the my order should have 1 item", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 32
   testRunner.And("the my order contains product E831967C-622E-4804-87B5-BDE90B37F5C4 with amount 7." +
                    "7", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Remove single item")]
        [Xunit.TraitAttribute("FeatureTitle", "OrderItems")]
        [Xunit.TraitAttribute("Description", "Remove single item")]
        public virtual void RemoveSingleItem()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Remove single item", null, ((string[])(null)));
#line 34
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 35
 testRunner.Given("I have running ordering service", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 36
   testRunner.And("I created my order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 37
   testRunner.And("I added product E831967C-622E-4804-87B5-BDE90B37F5C4 to my order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 38
  testRunner.When("I remove product E831967C-622E-4804-87B5-BDE90B37F5C4 from my order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 39
  testRunner.Then("the my order should not have any items", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Remove one of items")]
        [Xunit.TraitAttribute("FeatureTitle", "OrderItems")]
        [Xunit.TraitAttribute("Description", "Remove one of items")]
        public virtual void RemoveOneOfItems()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Remove one of items", null, ((string[])(null)));
#line 41
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 42
 testRunner.Given("I have running ordering service", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 43
   testRunner.And("I created my order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 44
   testRunner.And("I added product E831967C-622E-4804-87B5-BDE90B37F5C4 to my order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 45
   testRunner.And("I added product 0C0A5849-AF60-4EA7-A093-32B25A3D3E36 to my order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 46
  testRunner.When("I remove product E831967C-622E-4804-87B5-BDE90B37F5C4 from my order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 47
  testRunner.Then("the my order should have 1 item", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 48
   testRunner.And("the my order should contain product 0C0A5849-AF60-4EA7-A093-32B25A3D3E36", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.4.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                OrderItemsFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                OrderItemsFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
