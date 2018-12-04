Feature: Orders
	As a consumer,
	I want to manage orders,
	so that I can build an ordering functionality
	
Scenario: Add new order
	Given I have running ordering service
	 When I create new my order
	 Then the service should list the my order
	  And the service should provide details about my order

Scenario: Clearing an order
	Given I have running ordering service
	  And I created my order
	  And I added product E831967C-622E-4804-87B5-BDE90B37F5C4 to my order
	 When I clear my order
	 Then the service should list the my order
	  And the my order should not have any items

Scenario: Deleting a single order
	Given I have running ordering service
	  And I created my order
	  And I added product E831967C-622E-4804-87B5-BDE90B37F5C4 to my order
	 When I delete my order
	 Then the service should not list my order

Scenario: Deleting one of the orders
	Given I have running ordering service
	  And I created first order
	  And I added product E831967C-622E-4804-87B5-BDE90B37F5C4 to first order
	  And I created second order
	  And I added product 0C0A5849-AF60-4EA7-A093-32B25A3D3E36 to second order
	 When I delete first order
	 Then the service should not list first order
	  And the service should list the second order
