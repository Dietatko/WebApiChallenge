Feature: Orders
	As a consumer,
	I want to manage orders,
	so that I can build an ordering functionality

Scenario: List orders
	Given I have running ordering service
	 Then there are no existing orders
