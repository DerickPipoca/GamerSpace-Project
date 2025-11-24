import { CartItem } from './../../shared/models/cart-item.model';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { ProductVariant } from '../../shared/models/product-variant.model';
import { Product } from '../../shared/models/product.model';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private readonly CART_STORAGE_KEY = 'gamer-space-cart';

  private cartItemsSubject = new BehaviorSubject<CartItem[]>([]);

  public cartItems$: Observable<CartItem[]> = this.cartItemsSubject.asObservable();

  constructor() {
    this.loadCartFromStorage();
  }

  loadCartFromStorage(): void {
    const cartJson = localStorage.getItem(this.CART_STORAGE_KEY);
    if (cartJson) {
      const items = JSON.parse(cartJson) as CartItem[];
      this.cartItemsSubject.next(items);
    }
  }

  private saveAndNotify(items: CartItem[]): void {
    const cartJson = JSON.stringify(items);

    localStorage.setItem(this.CART_STORAGE_KEY, cartJson);

    this.cartItemsSubject.next(items);
  }

  public addToCart(product: Product | null, variant: ProductVariant, quantity: number): void {
    const currentItems = [...this.cartItemsSubject.value];

    const existingItem = currentItems.find((item) => item.variant.id === variant.id);

    if (existingItem) {
      existingItem.quantity += quantity;
    } else if (product != null) {
      currentItems.push({ product, variant, quantity });
    }
    console.log(product);
    this.saveAndNotify(currentItems);
  }

  public removeFromCart(variantId: number): void {
    const currentItems = this.cartItemsSubject.value.filter(
      (item) => item.variant.id !== variantId,
    );
    this.saveAndNotify(currentItems);
  }

  public updateFromCart(variantId: number, newQuantity: number): void {
    const currentItems = [...this.cartItemsSubject.value];
    const itemToUpdate = currentItems.find((item) => item.variant.id === variantId);

    if (itemToUpdate) {
      if (newQuantity > 0) {
        itemToUpdate.quantity = newQuantity;
      } else {
        this.removeFromCart(variantId);
        return;
      }
    }
    this.saveAndNotify(currentItems);
  }

  public clearCart(): void {
    this.saveAndNotify([]);
  }
}
