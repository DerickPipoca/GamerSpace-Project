import { CheckoutDto } from './../../shared/models/checkout.model';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { CartItem } from '../../shared/models/cart-item.model';
import { Observable } from 'rxjs';
import { Order } from '../../shared/models/order.model';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private readonly apiUrl = `${environment.apiUrl}/api/orders`;

  constructor(private http: HttpClient) {}

  public checkout(cartItems: CartItem[]): Observable<Order> {
    const checkoutItems = cartItems.map((item) => ({
      productVariantId: item.variant.id,
      quantity: item.quantity,
    }));

    const checkoutDto: CheckoutDto = {
      items: checkoutItems,
    };

    return this.http.post<Order>(`${this.apiUrl}/checkout`, checkoutDto);
  }
}
