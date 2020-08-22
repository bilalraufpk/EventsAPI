--///// Select max order amount of a event and fetch event, order and buyer details. All orders of that event with max amount should be included.
--///// I used CTE, we can use any temp storage technique like temp variable/table etc
--///// Option 1
with EventOrders as(
select e.*, (select max(Amount) from [order] o where o.EventId=e.Id) MaxOrderAmount from [Event] e where isnull(e.IsDeleted,0)<>1
)

select e.Title, e.Date EventDate, o.Amount, o.DateTime OrderDate, u.FirstName BuyerFirstName,u.LastName BuyerLastName,u.Email BuyerEmail  from EventOrders e
inner join [Order] o on e.Id=o.EventId and o.Amount=e.MaxOrderAmount
inner join [User] u on u.Id=o.UserId

--///// end Option 1

--///// Option 2
with EventOrders as(
select EventId, max(Amount) MaxOrderAmount from [Order] o group by EventId
)

select e1.Title, e1.Date EventDate, o.Amount, o.DateTime OrderDate, u.FirstName BuyerFirstName,u.LastName BuyerLastName,u.Email BuyerEmail  from EventOrders e
inner join [Event] e1 on e1.id=e.eventId and isnull(e1.IsDeleted,0)<>1
inner join [Order] o on e.EventId=o.EventId and o.Amount=e.MaxOrderAmount
inner join [User] u on u.Id=o.UserId
--///// end Option 2