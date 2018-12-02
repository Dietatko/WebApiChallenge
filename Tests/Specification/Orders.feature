Feature: Orders
	As a consumer,
	I want to manage orders,
	so that I can build an ordering functionality

Scenario: Lists no orders on start
	Given I have running ordering service
	 Then there are no existing orders

Scenario: Add new order
	Given I have running ordering service
	 When I create a new my order
	 Then the service lists my order
	  And the service provides details about my order
