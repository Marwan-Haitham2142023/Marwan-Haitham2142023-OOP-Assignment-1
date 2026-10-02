# Critique of the C++ Order System

I read through the code a few times before writing this. It runs fine for the demo, but I think it's fragile, and most of the problems come from the same place: there are no real "things" in the program, only lists of loose values.

## How the data is organized

The biggest issue is the parallel arrays. A customer is really five separate arrays (`customerIds`, `customerNames`, `customerEmails`, `customerCities`, `customerIsVip`) that only belong together because they use the same index. Nothing checks that. If someone adds a new field and forgets to update one of the functions, the arrays quietly drift apart and nobody gets an error. It also means you can never hand "a customer" to a function. You pass an ID and the function has to search for it again. Products and orders have the same problem.

Order lines are stored in two big 2D arrays, 100 × 20 slots each, whether or not they're used. Most orders here have one or two lines, so nearly all of that space is wasted. And again, the two arrays only work if they stay lined up.

Another thing that worried me is that orders don't store a customer, they store a position in the customer array. Lines store a product position the same way. That's fine until someone deletes or sorts a customer or product. Then every saved position points at the wrong record, and an old order shows the wrong customer or product. Since IDs and positions are both plain `int`s, the compiler can't warn you if you mix them up.

## Global state and hard limits

Everything lives in global variables, so any function can change anything. Looking at a function signature tells you nothing about what it touches. You also can't run two separate systems, test one piece alone, or reset things between tests.

The size limits (50 customers, 50 products, 100 orders) are hard-coded too. Past that point the system just stops accepting data, and all you get is a printed message.

## Business logic problems

This is the section I care most about, because these bugs give wrong answers silently.

Prices aren't saved on order lines. The total is calculated from the current product price every time. So if a price changes next month, every past order, including paid ones, gets a new total. The VIP discount works the same way, since it checks whether the customer is VIP now. Change someone's VIP status and their old orders change, and so does `totalSalesPaidOnly()`. A sale should record what was actually charged at the time.

Stock handling has gaps as well. Stock is taken off as soon as a line is added, not when the order is paid. There's no way to remove a line or cancel an order, so an abandoned order keeps that stock locked forever, and a mistaken line can't be undone. Once an order is paid it's locked completely, so mistakes there can't be fixed either.

Money is stored as `double`. Values like 0.10 can't be represented exactly in binary floating point, so small rounding errors creep in, and multiplying by `0.90` adds to that. Money should be kept as whole minor units (piastres) or a decimal type.

There are smaller rule problems too. The 10% discount is a bare `0.90` inside `calculateOrderTotal`. The printed order shows line totals but no discount line, so for VIPs the TOTAL doesn't match what you get by adding up the lines above it. Adding the same product twice makes two separate lines instead of merging them. And marking an already-paid order as paid is silently accepted.

## Error handling and input

The business rules and the console output are mixed together. Functions like `addLineToOrder` both apply the rules and print to `cout`, so you can't reuse them in a GUI, a web service or a test without dragging the printing along.

Almost every function returns `void` and reports failure by printing, so the caller has no idea whether it worked. In `runDemoScenario`, if `createOrder` fails, the next calls fail as well and the program carries on regardless. Only `createOrder` returns `-1` on failure, so even the error style is inconsistent.

The menu never checks whether `cin` succeeded. If a user types a letter where a number is expected, `cin` goes into a fail state and every later read fails with it. `choice` ends up as 0 and the program exits on its own with no explanation. Dates are plain strings, so "banana" is accepted as a date, and emails aren't checked either. The menu can't add customers or products at all, and `cin >> string` wouldn't handle names with spaces anyway.

Finally, some small inefficiencies: `printAllOrders` gets each order's ID and then `printOrder` searches for it again, every lookup is a linear scan, and strings are passed by value instead of `const&`.