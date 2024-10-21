import { Route, Routes } from 'react-router-dom';
import { HomePage } from './components/home';
import { LoginPage } from './components/login';
import { MainLayout } from './layout';

function App() {
  return (
    <Routes>
      <Route path='/login' element={<LoginPage x="aa" />}></Route>
      <Route path='/' element={<MainLayout><HomePage /></MainLayout>}></Route>
    </Routes>
  );
}

export default App;
