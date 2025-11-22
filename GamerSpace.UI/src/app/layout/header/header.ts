import { Component } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-header',
  imports: [MatIcon, RouterLink],
  templateUrl: './header.html',
  styleUrl: './header.scss',
})
export class Header {
  constructor(private router: Router) {}
  searchItem(text: string) {
    if (text.length > 0) {
      this.router.navigate(['products'], { queryParams: { searchTerm: text } });
    } else {
      this.router.navigate(['products']);
    }
  }
}
