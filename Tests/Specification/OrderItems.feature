Feature: OrderItems
	As a consumer,
	I want to manage items in an order,
	so that I can build an ordering functionality

Scenario: Newly created order has no items
	Given I have running ordering service
	 When I create a new my order
	 Then the my order has no items

Scenario: Add new item
	Given I have running ordering service
	  And I creates a new my order
	 When I add product E831967C-622E-4804-87B5-BDE90B37F5C4 to my order
	 Then the my order has 1 item
	  And the my order contains product E831967C-622E-4804-87B5-BDE90B37F5C4

Scenario: Update item amount
	Given I have running ordering service
	  And I creates a new my order
	 When I add product E831967C-622E-4804-87B5-BDE90B37F5C4 with amount 2.5 to my order
	  And I update amount of product E831967C-622E-4804-87B5-BDE90B37F5C4 in my order to 4.6
	 Then the my order has 1 item
	  And the my order contains product E831967C-622E-4804-87B5-BDE90B37F5C4 with amount 4.6

Scenario: Amount is summed if same product is added
	Given I have running ordering service
	  And I creates a new my order
	 When I add product E831967C-622E-4804-87B5-BDE90B37F5C4 with amount 2.2 to my order
	  And I add product E831967C-622E-4804-87B5-BDE90B37F5C4 with amount 5.5 to my order
	 Then the my order has 1 item
	  And the my order contains product E831967C-622E-4804-87B5-BDE90B37F5C4 with amount 7.7
