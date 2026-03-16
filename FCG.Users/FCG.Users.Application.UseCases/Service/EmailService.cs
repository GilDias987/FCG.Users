using FCG.Users.Application.Dto.Email;
using FCG.Users.Application.Interface.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Users.Application.UseCases.Service
{
    public class EmailService : IEmailService
    { 
        public EmailMessageDto EmailMessage(string email, string name)
        {
            var subject = "FCG - Bem-vindo(a)!";
            var body = $@"<!DOCTYPE html>
                                <html lang=""pt-br"">
                                <head>
                                    <meta charset=""UTF-8"">
                                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                    <style>
                                        /* Estilos Base */
                                        body {{ font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background-color: #0b0e11; color: #ffffff; margin: 0; padding: 0; }}
                                        .email-container {{ max-width: 600px; margin: 20px auto; background-color: #161a1e; border: 1px solid #2d333b; border-radius: 12px; overflow: hidden; }}
        
                                        /* Cabeçalho */
                                        .header {{ background-color: #1c2128; padding: 30px; text-align: center; border-bottom: 4px solid #f39c12; }}
                                        .logo {{ width: 160px; height: auto; margin-bottom: 10px; }}
        
                                        /* Corpo */
                                        .content {{ padding: 20px 20px; }}
                                        h2 {{ color: #f39c12; margin-top: 0; }}
                                        p {{ line-height: 1.6; color: #cbd5e0; }}
        
                                        /* Tabela de Itens */
                                        .order-summary {{ width: 100%; border-collapse: collapse; margin-top: 25px; }}
                                        .order-summary th {{ text-align: left; color: #718096; font-size: 12px; text-transform: uppercase; padding-bottom: 10px; }}
                                        .order-summary td {{ padding: 12px 0; border-top: 1px solid #2d333b; }}
                                        .total-row {{ color: #f39c12; font-weight: bold; font-size: 18px; }}

                                        /* Botão */
                                        .btn-cta {{ display: inline-block; padding: 15px 35px; background-color: #f39c12; color: #0b0e11 !important; text-decoration: none; border-radius: 6px; font-weight: bold; margin-top: 30px; }}
        
                                        /* Rodapé */
                                        .footer {{ padding: 25px; text-align: center; font-size: 12px; color: #4a5568; background-color: #0b0e11; }}
                                    </style>
                                </head>
                                <body>
                                    <div class=""email-container"">
                                        <div class=""header"">
                                            <h1 style=""color: #f39c12; margin: 0;"">FCG GAMES</h1>
                                        </div>

                                        <div class=""content"">

                                            <p style=""font-size: 18px;"">Bem-vindo ao clã, <strong>{name}</strong>!</p>

                                            <p>Sua conta na <strong>FCG GAMES</strong> foi criada com sucesso. Agora você tem acesso antecipado às nossas ofertas e novidades do mundo gamer.</p>          

                                            <center>
                                                <a href=""[Link_Area_Membro]"" class=""btn-cta"">EXPLORAR LOJA</a>
                                            </center>
                                        </div>

                                        <div class=""footer"">
                                            <p>Este é um e-mail automático enviado por FCG GAMES.<br>
                                            Não responda a este e-mail. Para suporte, use: <a href=""mailto:fiapclound@gmail.com"" style=""color: #f39c12;"">fiapclound@gmail.com</a></p>
                                            <p>&copy; 2026 FCG GAMES - Level Up Your Life.</p>
                                        </div>
                                    </div>
                                </body>
                                </html>";

            return new EmailMessageDto { 
                                         To = email, 
                                         Body = body, 
                                         Subject = subject };
        }
    }
}
