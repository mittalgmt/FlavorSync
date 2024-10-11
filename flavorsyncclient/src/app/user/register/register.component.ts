import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './register.component.html',
  styles: ``
})
export class RegisterComponent {
  constructor(public formBuilder: FormBuilder){}

  form = this.formBuilder.group({
    fullName : [''],
    email : [''],
    password : [''],
    confirempassword : [''],
    
  })

  onSubmit(){
    console.log(this.form);
  }
}
