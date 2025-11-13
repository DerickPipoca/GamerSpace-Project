import { Component } from '@angular/core';
import { FaIconLibrary, FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faCoffee, faEnvelope } from '@fortawesome/free-solid-svg-icons';
import {
  faFacebook,
  faXTwitter,
  faInstagram,
  faLinkedin,
} from '@fortawesome/free-brands-svg-icons';

@Component({
  selector: 'app-footer',
  imports: [FontAwesomeModule],
  templateUrl: './footer.html',
  styleUrl: './footer.scss',
})
export class Footer {
  constructor(lib: FaIconLibrary) {
    lib.addIcons(faCoffee, faFacebook, faXTwitter, faEnvelope, faInstagram, faLinkedin);
  }
}
