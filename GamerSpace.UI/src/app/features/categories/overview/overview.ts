import { RouterLink } from '@angular/router';
import { CategoryData } from '../../../shared/models/category-data.model';
import { Component } from '@angular/core';
import { CategoryCard } from '../../../layout/category-card/category-card';

@Component({
  selector: 'app-overview',
  imports: [CategoryCard],
  templateUrl: './overview.html',
  styleUrl: './overview.scss',
})
export class CategoriesOverview {
  categoriesList: CategoryData[] = [];
  constructor() {
    this.categoriesList = [
      {
        name: 'Mouses',
        imageUrl: '../../../assets/images/illustrations/category_mouse.png',
        categoryId: 7,
      },
      {
        name: 'Teclados',
        imageUrl: '../../../assets/images/illustrations/category_keyboard.png',
        categoryId: 8,
      },
      {
        name: 'Mouse pads',
        imageUrl: '../../../assets/images/illustrations/category_mousepad.png',
        categoryId: 9,
      },
      {
        name: 'Headsets',
        imageUrl: '../../../assets/images/illustrations/category_headset.png',
        categoryId: 10,
      },
      {
        name: 'Kits',
        imageUrl: '../../../assets/images/illustrations/category_kit.png',
        categoryId: 11,
      },
      {
        name: 'Caixas de som',
        imageUrl: '../../../assets/images/illustrations/category_sound.png',
        categoryId: 12,
      },
      {
        name: 'Microfones',
        imageUrl: '../../../assets/images/illustrations/category_microphone.png',
        categoryId: 13,
      },
      {
        name: 'Webcams',
        imageUrl: '../../../assets/images/illustrations/category_webcam.png',
        categoryId: 14,
      },
    ];
  }
}
