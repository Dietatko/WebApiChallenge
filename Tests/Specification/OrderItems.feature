Feature: OrderItemss
	As a consumer,
	I want to manage items in an order,
	so that I can build an ordering functionality

Scenario: Newly created order has no items
	Given I have running ordering service
	 When I create a new my order
	 Then the my order has no items
