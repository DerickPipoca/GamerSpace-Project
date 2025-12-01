export interface ProductVariant {
  id: number;
  sku: string;
  price: number;
  description: string | null;
  imageUrl: string | null;
  stockAmount: number;
}
