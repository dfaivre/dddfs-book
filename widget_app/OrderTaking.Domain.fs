
namespace OrderTaking.Domain

type Undefined = exn

type WidgetCode = WidgetCode of string
type GizmoCode = GizmoCode of string
type ProductCode =
    | Widget of WidgetCode
    | Gizmo of GizmoCode

type UnitQuantity = UnitQuantity of int
type KilogramQuantity = KilogramQuantity of decimal
type OrderQuantity = 
    | Unit of UnitQuantity
    | Kilogram of KilogramQuantity

type ProductCatelog = Undefined

type EnvelopeContents = EnvelopeContents of string
type QuoteForm = Undefined
type OrderForm = Undefined
type CategorizedMail =
    | Quote of QuoteForm
    | Order of OrderForm
type CategorizeInboundMail = EnvelopeContents -> CategorizedMail


(*
    Orders
*)

type OrderId = Undefined
type OrderLineId = Undefined
type CustomerId = Undefined

type CustomerInfo = Undefined
type ShippingAddress = Undefined
type BilllingAddress = Undefined
type BillingAmount = Undefined
type Price = Undefined

type Order = {
    Id: OrderId
    CustomerId: CustomerId
    ShippingAddress : ShippingAddress
    BillingAddress : BilllingAddress
    OrderLines : OrderLine list
    AmountToBill : BillingAmount
}
and OrderLine = {
    Id: OrderLineId
    OrderId: OrderId
    ProductCode: ProductCode
    OrderQuantity: OrderQuantity
    Price: Price
}

type UnvalidatedOrder = Undefined
type ValidatedOrder = Undefined
type PricedOrder = Undefined

type ValidationError = {
    FieldName:string
    ErrorDescription:string
}
type ValidationResponse<'a> = Async<Result<'a, ValidationError list>>
type ValidateOrder = UnvalidatedOrder -> ValidationResponse<ValidatedOrder>


type AcknowledgementSent = Undefined
type OrderPlaced = Undefined
type BillableOrderPlaced = Undefined

type PlaceOrderEvents = {
    AcknowledgementSent : AcknowledgementSent
    OrderPlaced : OrderPlaced
    BillableOrderPlaced : BillableOrderPlaced
}
type PlaceOrderError = 
    | ValidationError of ValidationError
    // | ... others

type PlaceOrderResult = Result<PlaceOrderEvents, PlaceOrderError>
type PlaceOrder = UnvalidatedOrder -> PlaceOrderResult

type CalculatePrices = OrderForm -> ProductCatelog -> PricedOrder
