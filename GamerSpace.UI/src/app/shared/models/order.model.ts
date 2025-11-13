import { OrderProduct } from './order-product.model';

export interface Order {
  id: number;
  createTime: Date;
  status: string;
  shippingAmount: number;
  subTotalAmount: number;
  discountAmount: number;
  userId: number;
  orderProductsP: OrderProduct[];
}
