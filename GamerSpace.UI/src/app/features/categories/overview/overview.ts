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
        pathUrl: '/',
      },
      {
        name: 'Teclados',
        imageUrl: '../../../assets/images/illustrations/category_keyboard.png',
        pathUrl: '/',
      },
      {
        name: 'Mouse pads',
        imageUrl: '../../../assets/images/illustrations/category_mousepad.png',
        pathUrl: '/',
      },
      {
        name: 'Headsets',
        imageUrl: '../../../assets/images/illustrations/category_headset.png',
        pathUrl: '/',
      },
      {
        name: 'Kits',
        imageUrl: '../../../assets/images/illustrations/category_kit.png',
        pathUrl: '/',
      },
      {
        name: 'Caixas de som',
        imageUrl: '../../../assets/images/illustrations/category_sound.png',
        pathUrl: '/',
      },
      {
        name: 'Microfones',
        imageUrl: '../../../assets/images/illustrations/category_microphone.png',
        pathUrl: '/',
      },
      {
        name: 'Webcams',
        imageUrl: '../../../assets/images/illustrations/category_webcam.png',
        pathUrl: '/',
      },
    ];
  }
}
