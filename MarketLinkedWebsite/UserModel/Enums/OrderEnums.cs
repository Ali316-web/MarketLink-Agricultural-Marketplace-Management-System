namespace LocalFarmersMarket.Enums
{
    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }

    public enum FarmerStatus
    {
        PendingApproval,
        Active,
        Suspended,
        Rejected
    }

    public enum PaymentStatus
    {
        Pending,
        Paid,
        Failed,
        Refunded
    }

    public enum UnitType
    {
        Kg,
        Gram,
        Litre,
        Piece,
        Dozen,
        Bundle
    }
}