import { ProductVariant } from './product-variant.model';
import { Product } from './product.model';

export interface CartItem {
  product: Product;
  variant: ProductVariant;
  quantity: number;
}
