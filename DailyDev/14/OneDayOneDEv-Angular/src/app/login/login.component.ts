import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { User } from '../../models/User';
import { AuthService } from '../../services/auth.service';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [FormsModule,
  RouterModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  constructor(private authService: AuthService,private router : Router) { }

  
  onSubmit(form: NgForm) {

    if (!form.valid) return;

     let user : User = new User(
      form.value.username,
      form.value.password
    );

    console.log(user);

    this.authService.login(user).subscribe({
      next: (response) => {

        const token = response.access_token;
        const expireIn = response.expires_in;

        const expirationDate = new Date();
        expirationDate.setSeconds(expirationDate.getSeconds() + expireIn);

        localStorage.setItem('token', token);
        localStorage.setItem('tokenExpiration', expirationDate.toISOString());
        
        this.router.navigate(["/Task"]);

      },
      error: (err) => {
        alert("Erreur login " + err);
      }
    });

  }

  ngOnInit(): void {
    const isLogged = this.authService.isLoggedIn();

    if (isLogged) {
      alert('Déjà connecté');
    }
  }

}


