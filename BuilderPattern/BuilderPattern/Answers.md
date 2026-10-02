# Task 3.1 — Class Design

## Question 1

### ليه Constructor فيه 20 Parameter مشكلة في الواقع؟

أول مشكلة إن الـ **call site** بيبقى صعب جدًا في القراءة. لما أشوف حاجة زي:

```csharp
new Invoice(
    1001,
    "Ali Hassan",
    "ali@mail.com",
    "0100000000",
    "12 Nile St",
    "Cairo",
    "Cairo",
    "11511",
    "Egypt",
    ...
);
```

مش هقدر أعرف بسهولة كل قيمة بتخص أنهي property، وممكن أضطر أرجع لتعريف الـ constructor وأعد الـ parameters واحد واحد.

المشكلة الأكبر إن ممكن أبعت قيم في المكان الغلط من غير ما الـ compiler يكتشف ده.

مثلًا لو عندي:

```csharp
decimal subTotal
decimal discount
decimal tax
decimal total
```

كلهم نفس النوع `decimal`، فلو بدلت `discount` و `tax`، الكود هيعمل compile عادي، لكن البيانات نفسها هتبقى غلط.

ونفس الكلام مع الـ addresses، لأن عندي كذا `string` ورا بعض، فممكن مثلًا أحط `ShippingCity` مكان `BillingCity` والكود يشتغل عادي.

كمان لو أضفنا property جديدة اختيارية بعدين، الـ constructor هيكبر أكتر، وكل مكان بيستخدمه ممكن يحتاج تعديل. ومع الوقت الـ constructor هيبقى أصعب في الاستخدام والصيانة.

---

## Question 2

### هل المشكلة بس إن الـ Constructor طويل؟

لا، المشكلة أعمق من كده.

المشكلة إن الـ `Invoice` class نفسه بيجمع **حاجات مختلفة مش مرتبطة ببعض بشكل قوي**.

عندنا مثلًا:

* بيانات الـ customer.
* الـ billing address.
* الـ shipping address.
* بيانات الـ order.
* بيانات الـ payment.
* المبالغ والـ taxes والـ discounts.

الـ Address لوحده يعتبر concept مستقل وله قواعد validation خاصة بيه، زي إن الـ Street والـ City والـ Zip Code والـ Country مينفعش يكونوا فاضيين.

وفي نفس الوقت الـ Order والـ Payment ليهم قواعد مختلفة تمامًا.

فلو حطينا كل حاجة في `Invoice`، أي تغيير في قواعد الـ Address مثلًا هيأثر على class المفروض إنه مسؤول عن الفاتورة كلها.

وده ضد فكرة **Single Responsibility**، لأن الكلاس بقى مسؤول عن حاجات كتير.

---

# Task 3.2 — Builder Pattern

استخدمنا **Fluent Builder Pattern** عشان نخلي إنشاء الـ Invoice أوضح وأسهل في القراءة.

بدل ما نبعت 20 parameter مرة واحدة، بنستخدم methods واضحة زي:

```csharp
.SetCustomerName("Ali Hassan")
.SetPaymentMethod("Credit Card")
.SetSubTotal(1000)
.SetTaxAmount(95)
```

وده بيخلي واضح كل value بتخص إيه.

قسمنا الـ properties إلى **mandatory** و **optional**.

الـ mandatory properties هي البيانات اللي مينفعش الـ Invoice يتعمل من غيرها، زي:

* Invoice ID
* Customer Name
* Customer Email
* Billing Address
* Shipping Address
* Order Date
* Payment Method
* Currency
* SubTotal
* Total Amount

أما حاجات زي:

* Customer Phone
* Discount
* Tax

فممكن يكون ليها default value أو تكون optional حسب تصميم النظام.

وعند استدعاء `Build()` بنعمل validation، ولو mandatory property ناقصة، بنطلع error واضح بدل ما نطلع Invoice ناقص.

مثال:

```csharp
if (billingAddress == null)
    throw new InvalidOperationException(
        "Billing address is mandatory.");
```

---

# Task 3.3 — Composed Builders

بدل ما يكون عندنا Builder واحد مسؤول عن كل حاجة، قسمناه إلى Builders أصغر:

```text
InvoiceBuilder
│
├── AddressBuilder → Billing Address
│
├── AddressBuilder → Shipping Address
│
└── OrderBuilder → Order / Payment Information
```

## 1. Single Responsibility

كل Builder بقى مسؤول عن حاجة واحدة بس.

`AddressBuilder` مسؤول عن إنشاء والتحقق من الـ Address.

`OrderBuilder` مسؤول عن إنشاء والتحقق من بيانات الـ Order والـ Payment.

أما `InvoiceBuilder` فدوره الأساسي إنه يجمع الأجزاء دي مع بعض ويعمل الـ final `Invoice`.

كده كل class عنده responsibility واضحة بدل ما يكون عندنا Builder واحد ضخم.

---

## 2. Independent Validation

أيوه، `AddressBuilder` يقدر يعمل validation للـ Address لوحده.

مثلًا:

```csharp
if (string.IsNullOrWhiteSpace(Street))
    throw new InvalidOperationException("Street is mandatory.");

if (string.IsNullOrWhiteSpace(City))
    throw new InvalidOperationException("City is mandatory.");

if (string.IsNullOrWhiteSpace(ZipCode))
    throw new InvalidOperationException("ZipCode is mandatory.");

if (string.IsNullOrWhiteSpace(Country))
    throw new InvalidOperationException("Country is mandatory.");
```

الـ `InvoiceBuilder` مش محتاج يعرف تفاصيل validation بتاعة الـ Address.

هو بس محتاج يعرف إن عنده `Address` جاهز وصحيح.

وده بيخلي كل جزء يقدر يختبر ويتطور بشكل مستقل.

---

## 3. Reuse

نفس `AddressBuilder` بيتستخدم للـ Billing Address والـ Shipping Address.

مثلًا:

```csharp
.WithBillingAddress(a => a
    .WithStreet("12 Nile St")
    .WithCity("Cairo")
    .WithZipCode("11511")
    .WithCountry("Egypt"))
```

ونستخدم نفس الـ Builder للـ Shipping:

```csharp
.WithShippingAddress(a => a
    .WithStreet("20 Main St")
    .WithCity("Giza")
    .WithZipCode("12511")
    .WithCountry("Egypt"))
```

لو مكنش عندنا `AddressBuilder`، كنا ممكن نضطر نكرر نفس validation والمنطق الخاص بالـ Address مرتين: مرة للـ Billing ومرة للـ Shipping.

وده ممكن يعمل مشكلة لو عدلنا validation في مكان ونسينا نعدله في المكان التاني.

---

## 4. Readability

في الـ single builder، كل حاجة كانت ممكن تبقى في سلسلة طويلة:

```csharp
new InvoiceBuilder()
    .WithInvoiceId(1001)
    .WithCustomer(...)
    .WithBillingAddress(...)
    .WithShippingAddress(...)
    .WithPayment(...)
    .WithAmounts(...)
    .Build();
```

أما في الـ composed version، كل مجموعة واضحة لوحدها:

```csharp
new InvoiceBuilder()
    .WithOrder(o => o
        .WithInvoiceId(1001)
        .WithCustomer(...)
        .WithPayment(...)
        .WithAmounts(...))
    .WithBillingAddress(a => a
        .WithStreet(...)
        .WithCity(...)
        .WithZipCode(...)
        .WithCountry(...))
    .WithShippingAddress(a => a
        .WithStreet(...)
        .WithCity(...)
        .WithZipCode(...)
        .WithCountry(...))
    .Build();
```

كده وأنا بقرأ الكود، أقدر أعرف بسهولة:

* إيه اللي خاص بالـ Order.
* إيه اللي خاص بالـ Billing Address.
* إيه اللي خاص بالـ Shipping Address.

فـ **composed builders** مش بس حل لمشكلة الـ constructor الطويل، لكنه كمان بيخلي الـ design نفسه أنضف وأسهل في الصيانة والاختبار وإعادة الاستخدام.
