import { Router } from '@angular/router';
import { AccountService } from './../account.service';
import { AsyncValidatorFn, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { of, timer } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup;
  errors: string[];

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private accountService: AccountService) { }

  ngOnInit(): void {
    this.createRegisterForm();
  }

  createRegisterForm(): void {
    this.registerForm = this.fb.group({
      displayName: [null, [Validators.required]],
      email: [null, [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')], [this.validateEmailNotTaken()]],
      password: [null, [Validators.required]]
    });
  }

  onSubmit(): void {
    this.accountService.register(this.registerForm.value).subscribe(() => this.router.navigateByUrl('/shop'), error => this.errors = error.errors)
  }

  validateEmailNotTaken(): AsyncValidatorFn {
    return control => timer(500).pipe(switchMap(() => control.value ? this.accountService.checkEmailExists(control.value).pipe(map(res => res ? { emailExists: true } : null)) : of(null)))
  }
}
