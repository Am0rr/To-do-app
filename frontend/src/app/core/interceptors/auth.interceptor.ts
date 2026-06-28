import { HttpInterceptorFn, HttpStatusCode } from "@angular/common/http";
import { inject } from "@angular/core";
import { AuthService } from "../services/auth.service";
import { Router } from "@angular/router";
import { catchError, switchMap, throwError } from "rxjs";

export const authInterceptor: HttpInterceptorFn = (req, next) => {
    const authService = inject(AuthService);
    const router = inject(Router);

    return next(req).pipe(
        catchError(error => {
            if(error.status == HttpStatusCode.Unauthorized) {
                const refreshToken = authService.getRefreshToken();

                if(refreshToken) {
                    return authService.refresh(refreshToken).pipe(
                        switchMap(response => {
                            authService.saveTokens(response);

                            const retryReq = req.clone({
                                setHeaders: {
                                    Authorization: `Bearer ${response.accessToken}`
                                }
                            });

                            return next(retryReq);
                        }),
                        catchError(refreshError => {
                            authService.logout();
                            router.navigate(['/login']);
                            return throwError(() => refreshError);
                        })
                    );
                }

                authService.logout();
                router.navigate(['/login']);
            }

            return throwError(() => error);
        })
    );
};