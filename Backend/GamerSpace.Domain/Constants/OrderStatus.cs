namespace GamerSpace.Domain.Constants
{
    public static class OrderStatus
    {
        public const string PendingPayment = "Pagamento pendente";
        public const string PaymentApproved = "Pagamento aprovado";
        public const string Processing = "Processando";
        public const string Shipped = "Enviado";
        public const string Delivered = "Entregue";
        public const string Canceled = "Cancelado";
    }
}