import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import Login from './pages/Login.tsx'
import Home from './pages/Home.tsx';
import { MaterialUiRTLProvider } from './MaterialUiRTLProvider.tsx';
import QueryClientProvider from './QueryClientProvider.tsx';
import { ReduxProvider } from './GlobalRedux/provider.tsx';
import CustomThemeProvider from './MaterialUiThemeProvider.tsx';
import { Toaster } from 'react-hot-toast';
import Register from './pages/Register.tsx';
import LogOut from './pages/LogOut.tsx';
import ProtectedRoute from './pages/ProtectedRoute.tsx';



    const router = createBrowserRouter([
        {   
            element: <ProtectedRoute />,
            children: [{
                path: "/",
                element:<Home/>
            }]
        },
        {
            path: "/Login",
            element: <Login />,
        },
        {
            path: "/Register",
            element: <Register/>
        },
        {
            path: "/LogOut",
            element: <LogOut/>
        }
    ]);


function App() {



    return (

        <>
            <MaterialUiRTLProvider>
                <QueryClientProvider>
                    <ReduxProvider>
                        <CustomThemeProvider>
                            <RouterProvider router={router} />
                        </CustomThemeProvider>
                    </ReduxProvider>
                    <Toaster 
                        position="top-center"
                        gutter={12}
                        containerStyle={{ margin: "8px" }}
                        toastOptions={{
                            success: {
                                duration: 3000,
                                style: {
                                    fontSize: "16px",
                                    maxWidth: "500px",
                                    padding: "10px 24px",
                                    backgroundColor: "#46dc5c",
                                    color: "#000000",
                                },
                            },
                            error: {
                                duration: 5000,
                                style: {
                                    fontSize: "16px",
                                    maxWidth: "500px",
                                    padding: "10px 24px",
                                    backgroundColor: "#cc2525",
                                    color: "#000000",
                                },
                            },
                        }}
                    />
                    {/*<ReactQueryDevtools initialIsOpen={false} />*/}
                </QueryClientProvider>
            </MaterialUiRTLProvider>
         
        </>
    )


}

export default App;