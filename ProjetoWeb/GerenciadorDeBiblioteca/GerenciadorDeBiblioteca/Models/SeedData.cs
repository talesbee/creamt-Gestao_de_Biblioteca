using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GerenciadorDeBiblioteca.Data;
using System;
using System.Text;
using System.Linq;
using GerenciadorDeBiblioteca.Models;
using System.Security.Cryptography;

namespace GerenciadorDeBiblioteca.SeedData
{
    public static class SeedData
    {
        public static string GetMD5(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        public static void Initialize(IServiceProvider serviceProvider)
        {
            
            using (var context = new GerenciadorDeBibliotecaContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<GerenciadorDeBibliotecaContext>>()))
            {
                if (!context.BookState.Any())
                {
                    context.BookState.AddRange(
                        new BookState
                        {
                            Name = "Disponível",
                            Description = "Livro disponível para empréstimo.",
                        },
                        new BookState
                        {
                            Name = "Emprestado",
                            Description = "Livro emprestado no momento.",
                        },
                        new BookState
                        {
                            Name = "Extraviado",
                            Description = "Livro perdido/danificado.",
                        },
                        new BookState
                        {
                            Name = "Em manutenção",
                            Description = "Livro sendo consertado.",
                        }
                    );
                    context.SaveChanges();
                }

                if (!context.TipePerson.Any())
                {
                    context.TipePerson.AddRange(
                        new TipePerson
                        {
                            Name = "Autor",
                            Description = "Auto do Livro.",
                            DeadlineDays = 10
                        },
                        new TipePerson
                        {
                            Name = "Profissional",
                            Description = "Pessoa usa o livro no trabalho.",
                            DeadlineDays = 15
                        },
                        new TipePerson
                        {
                            Name = "Aluno",
                            Description = "Estudante.",
                            DeadlineDays = 10
                        },
                        new TipePerson
                        {
                            Name = "Outros",
                            Description = "Uma pessoa comum.",
                            DeadlineDays = 10
                        }
                    );
                    context.SaveChanges();
                }
                if (!context.MovimentState.Any())
                {
                    context.MovimentState.AddRange(
                        new MovimentState
                        {
                            Name = "Emprestado",
                            Description = "Livro esta emprestado."

                        },
                        new MovimentState
                        {
                            Name = "Atrasado",
                            Description = "livro passou da data de devolução!"

                        },
                        new MovimentState
                        {
                            Name = "Devolvido",
                            Description = "Livro já devolvido!"

                        }
                        );
                    context.SaveChanges();
                }


                if (!context.User.Any())
                {
                    context.User.AddRange(
                        new User
                        {
                            Name = "Administrador",
                            LoginPass= GetMD5("123"),
                            LoginUser="admin"
                        });
                    context.SaveChanges();
                }
              
            }
        }
    }
}