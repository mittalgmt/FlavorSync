import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { AbstractControl, FormBuilder, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { combineLatest } from 'rxjs';
import { FirstKeyPipe } from "../../shared/pipes/first-key.pipe";

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, FirstKeyPipe],
  templateUrl: './register.component.html',
  styles: ``
})
export class RegisterComponent {

  constructor(public formBuilder: FormBuilder){}
  isSubmitted:boolean = false;

  passwordMatchValidator: ValidatorFn = (control:AbstractControl) : null =>
  {
    const password = control.get('password');
    const confirempassword = control.get('confirmpassword')

    if(password && confirempassword && password.value != confirempassword.value)
      confirempassword?.setErrors({passwordMismatch:true})
    else
    confirempassword?.setErrors(null)
    return null;
  }


  form = this.formBuilder.group({
    fullName : ['',Validators.required],
    email : ['',[Validators.required,Validators.email]],
    password : ['',[
      Validators.required,
      Validators.minLength(6),
      Validators.pattern(/(?=.*[^a-zA-Z0-9])/)]],
    confirempassword : [''],
    
  },{validators:this.passwordMatchValidator})

 

  onSubmit(){
    this.isSubmitted = true;
    console.log(this.form);
  }

  hasDisplayableError(controlName: string):Boolean{
     const control = this.form.get(controlName);
     return Boolean(control?.invalid) &&
     (this.isSubmitted || Boolean(control?.touched)|| Boolean(control?.dirty))
  }
}
