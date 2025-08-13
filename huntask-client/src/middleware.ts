import { NextRequest, NextResponse } from 'next/server';
import { authorizationMiddleware } from './middlewares';

export function middleware(req: NextRequest): NextResponse | unknown {
  const authResult = authorizationMiddleware(req);
  if (authResult) {
    return authResult;
  }

  return NextResponse.next();
}

export const config = {
  matcher: ['/((?!_next/static|_next/image|favicon.ico).*)']
};
