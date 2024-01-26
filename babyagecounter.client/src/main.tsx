import React from 'react'
import ReactDOM from 'react-dom/client'

import RootLayout from './Layout.tsx'
import AgeCounter from './baby/AgeCounter.tsx'

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <RootLayout>
      <AgeCounter></AgeCounter>
    </RootLayout>
  </React.StrictMode>,
)
