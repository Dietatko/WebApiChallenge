Feature: Orders
	As a consumer,
	I want to manage orders,
	so that I can build an ordering functionality

Scenario: Lists no orders on start
	Given I have running ordering service
	 Then the service lists no existing orders

Scenario: Add new order
	Given I have running ordering service
	 When I create new my order
	 Then the service lists the my order
	  And the service provides details about my order

Scenario: Clearing an order
	Given I have running ordering service
	  And I created my order
	  And I added product E831967C-622E-4804-87B5-BDE90B37F5C4 to my order
	 When I clear my order
	 Then the service lists the my order
	  And the my order has no items

Scenario: Deleting a single order
	Given I have running ordering service
	  And I created my order
	  And I added product E831967C-622E-4804-87B5-BDE90B37F5C4 to my order
	 When I delete my order
	 Then the service lists no existing orders

Scenario: Deleting one of the orders
	Given I have running ordering service
	  And I created first order
	  And I added product E831967C-622E-4804-87B5-BDE90B37F5C4 to first order
	  And I created second order
	  And I added product 0C0A5849-AF60-4EA7-A093-32B25A3D3E36 to second order
	 When I delete first order
	 Then the service lists 1 order
	 Then the service lists the second order
