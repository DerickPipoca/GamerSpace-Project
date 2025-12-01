export interface Product {
  id: number;
  name: string;
  description: string | null;
  createTime: Date;
  price: number;
  imageUrl: string | null;
}
