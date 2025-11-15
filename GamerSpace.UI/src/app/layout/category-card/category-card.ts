import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-category-card',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './category-card.html',
  styleUrl: './category-card.scss',
})
export class CategoryCard {
  @Input() categoryName: String = 'Mouses';
  @Input() categoryImageUrl: String = '../../../assets/images/illustrations/category_mouse.png';
  @Input() categoryPathUrl: String = '/';
}
