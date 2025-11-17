import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-product-card',
  imports: [RouterLink],
  templateUrl: './product-card.html',
  styleUrl: './product-card.scss',
})
export class ProductCard {
  @Input() productName: String = 'Mouses';
  @Input() productImageUrl: String = '/';
  @Input() productPrice: number = 0;
  @Input() productId: number = 0;
}
