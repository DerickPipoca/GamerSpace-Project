import { ProductVariant } from './product-variant.model';

export interface CartItem {
  variant: ProductVariant;
  quantity: number;
}
