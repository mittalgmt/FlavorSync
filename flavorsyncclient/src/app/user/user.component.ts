import { Component } from '@angular/core';
import { RegisterComponent } from './register/register.component';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [RegisterComponent],
  templateUrl: './user.component.html',
  styles: ``
})
export class UserComponent {

}
