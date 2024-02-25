import React from 'react'
import ReactDOM from 'react-dom/client'

import RootLayout from './Layout.tsx'
import AgeCounter from './baby/AgeCounter.tsx'
import Login from './auth/Login.tsx'

import { RouterProvider, createBrowserRouter } from 'react-router-dom'

const router = createBrowserRouter([
  {
    path: "/",
    element: <AgeCounter />
  },
  {
    path: "/login",
    element: <Login />
  }
])

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <RootLayout>
      <RouterProvider router={router} />
    </RootLayout>
  </React.StrictMode>,
)
