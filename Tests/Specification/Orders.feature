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
	 Then the service lists my order
	  And the service provides details about my order

Scenario: Clearing the order
	Given I have running ordering service
	  And I created my order
	  And I added product E831967C-622E-4804-87B5-BDE90B37F5C4 to my order
	 When I clear my order
	 Then the service lists my order
	  And the my order has no items
