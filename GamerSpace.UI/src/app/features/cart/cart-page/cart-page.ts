import { CartService } from './../../../core/services/cart-service';
import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CartItem } from '../../../shared/models/cart-item.model';
import { Subscription } from 'rxjs';
import { Router, RouterLink } from '@angular/router';
import { OrderService } from '../../../core/services/order-service';

@Component({
  selector: 'app-cart-page',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './cart-page.html',
  styleUrl: './cart-page.scss',
})
export class CartPage implements OnInit, OnDestroy {
  public cartItems: CartItem[] = [];
  private cartSubscription!: Subscription;
  public checkoutError: string | null = null;
  public isCheckingOut = false;

  constructor(
    private cartService: CartService,
    private orderService: OrderService,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.cartSubscription = this.cartService.cartItems$.subscribe((items) => {
      this.cartItems = items;
    });
  }
  ngOnDestroy(): void {
    if (this.cartSubscription) {
      this.cartSubscription.unsubscribe();
    }
  }

  removeFromCart(variantId: number): void {
    this.cartService.removeFromCart(variantId);
  }

  updateQuantity(variantId: number, quantity: number) {
    this.cartService.updateFromCart(variantId, quantity);
  }

  get totalPrice(): number {
    return this.cartItems.reduce((total, item) => {
      return total + item.variant.price * item.quantity;
    }, 0);
  }

  clearCart(): void {
    this.cartService.clearCart();
  }

  onCheckout(): void {
    this.checkoutError = null;
    this.isCheckingOut = true;

    this.orderService.checkout(this.cartItems).subscribe({
      next: (createdOrder) => {
        console.log('Pedido criado com sucesso!', createdOrder);
        this.isCheckingOut = false;

        this.cartService.clearCart();

        alert('Compra realizada com sucesso!');

        //Mudar para página de pedido concluído...
        this.router.navigateByUrl('/');
      },
      error: (err) => {
        this.checkoutError = err.error.detail || 'Falha ao processar o pedido.';
        this.isCheckingOut = false;
        console.error(err);
      },
    });
  }
}
