using Learning.DAL.Generation.Discovery;
using Learning.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Learning.DAL.RoslynGeneration
{
    public class BaseControllerGenerator
    {
        public void GenerateAsync()
        {
            var code = AssembleCode();
            var tree = SyntaxFactory.ParseSyntaxTree(code);
            string fileName = "Learning.DAL.Server.Generated.dll";
            // Detect the file location for the library that defines the object type
            var systemRefLocation = typeof(object).GetTypeInfo().Assembly.Location;
            var controllerRefLocation = typeof(ControllerBase).GetTypeInfo().Assembly.Location;
            // Create a reference to the libraries
            var systemReference = MetadataReference.CreateFromFile(systemRefLocation);
            var aspnetCoreReference = MetadataReference.CreateFromFile(controllerRefLocation);
            var systemRuntimeAssembly = MetadataReference.CreateFromFile(@"C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\3.1.0\ref\netcoreapp3.1\System.Runtime.dll");
            var persistanceReference = MetadataReference.CreateFromFile(@"bin/Debug/netcoreapp3.1/Learning.DAL.Models.dll"); 
            var generationReference = MetadataReference.CreateFromFile(@"bin/Debug/netcoreapp3.1/Learning.DAL.Generation.dll");
            var entityFrameworkReference = MetadataReference.CreateFromFile(@"bin/Debug/netcoreapp3.1/Microsoft.EntityFrameworkCore.dll");
            var compilation = CSharpCompilation.Create(fileName)
              .WithOptions(
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
              .AddReferences(systemReference, persistanceReference, generationReference, entityFrameworkReference, aspnetCoreReference, systemRuntimeAssembly)
              .AddSyntaxTrees(tree);
            string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            EmitResult compilationResult = compilation.Emit(path);
            if (compilationResult.Success)
            {
                Console.WriteLine($"Compilation success");
            }
            else
            {
                foreach (Diagnostic codeIssue in compilationResult.Diagnostics)
                {
                    string issue = $"ID: {codeIssue.Id}, Message: {codeIssue.GetMessage()}, Location: { codeIssue.Location.GetLineSpan()}, Severity: { codeIssue.Severity}";
                    Console.WriteLine(issue);
                }
            }
        }
        public string AssembleCode()
        {
            var apiModel = new DbContextParser(typeof(AdventureWorksContext)).Construct();
            var controllerCode = apiModel.Controllers.Select(controller => $@"
public class {controller.ResourceCollectionName}GeneratedController: BaseRoslynController<{controller.ResourceCollectionName}>
{{
    public {controller.ResourceCollectionName}GeneratedController(AdventureWorksContext dbContext):base(dbContext)
    {{

    }}
}}

");

            var aggregatedCode = $@"
using Learning.DAL.Generation.Mvc;
using Learning.DAL.Models.AdventureWorksModels;
using Learning.DAL.Models;

namespace Learning.DAL.Server.Generated
{{
    { controllerCode.Aggregate((final, next) => final += next) }
}}";
            return aggregatedCode;
        }

        #region Using the SyntaxGenerator (Work In Progress)
        public Task<SyntaxList<MemberDeclarationSyntax>> GenerateAsync(int y)//TransformationContext context, IProgress<Diagnostic> progress, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

            var generator = SyntaxGenerator.GetGenerator(new AdhocWorkspace(), LanguageNames.CSharp);
            var baseNode = generator.IdentifierName("BaseGeneratedController");

            var apiModel = new DbContextParser(typeof(AdventureWorksContext)).Construct();
            var classes = apiModel.Controllers.Select(controller =>
            {
                string controllerName = $"{controller.ResourceCollectionName}Controller";
                var members = CreateMembers(generator, controllerName);
                return generator.ClassDeclaration(
                    controllerName, typeParameters: null,
                    accessibility: Accessibility.Public,
                    modifiers: DeclarationModifiers.Abstract,
                    baseType: baseNode,
                    members: members);
            });
            // Generate the class
            ////// Our generator is applied to any class that our attribute is applied to.
            //var applyToClass = (ClassDeclarationSyntax)context.ProcessingNode;
            //var apiModel = new DbContextParser(typeof(GoodDogDbContext)).Construct();
            //var controllerCopies = apiModel.Controllers.Select(controller =>
            //{
            //    var genericType = SyntaxFactory.SingletonSeparatedList<TypeSyntax>(SyntaxFactory.ParseTypeName(controller.ResourceCollectionName));
            //    var newBaseList = applyToClass.BaseList.Types.Insert(0, SyntaxFactory.SimpleBaseType(
            //        SyntaxFactory.GenericName(SyntaxFactory.Identifier("BaseGeneratedController"))
            //                     .WithTypeArgumentList(SyntaxFactory.TypeArgumentList(genericType))));
            //    return applyToClass.WithIdentifier(SyntaxFactory.Identifier($"{controller.ResourceCollectionName}Controller"))
            //                       .WithBaseList(SyntaxFactory.BaseList(SyntaxFactory.SingletonSeparatedList(
            //                           SyntaxFactory.SimpleBaseType(SyntaxFactory.GenericName(SyntaxFactory.Identifier("BaseGeneratedController"))
            //                                                                     .WithTypeArgumentList(SyntaxFactory.TypeArgumentList(genericType))));
            //});
            ////var constructor = SyntaxFactory.ConstructorDeclaration("TestClientApi")
            ////            .WithInitializer(
            ////                SyntaxFactory.ConstructorInitializer(SyntaxKind.BaseConstructorInitializer)
            ////                    // could be BaseConstructorInitializer or ThisConstructorInitializer
            ////                    .AddArgumentListArguments(
            ////                        SyntaxFactory.Argument(SyntaxFactory.IdentifierName("entryPoint"))
            ////                    ));
            ////applyToClass.AddMembers(constructor);

            ////    // Return our modified copy. It will be added to the user's project for compilation.
            //var results = SyntaxFactory.List<MemberDeclarationSyntax>(controllerCopies);

            //return Task.FromResult(results);
        }
        public SyntaxNode[] CreateMembers(SyntaxGenerator generator, string className)
        {
            var dbContextField = generator.FieldDeclaration("_goodDogDbContext",
                generator.TypeExpression(SpecialType.System_Object),
                Accessibility.Private);
            var constructorParameters = new SyntaxNode[] {
                generator.ParameterDeclaration("goodDogDbContext",
                generator.TypeExpression(SpecialType.System_Object))
            };

            // Generate the constructor's method body
            var constructorBody = new SyntaxNode[] {
                generator.AssignmentStatement(generator.IdentifierName("_goodDogDbContext"),
                generator.IdentifierName("goodDogDbContext"))
            };

            // Generate the class' constructor
            var constructor = generator.ConstructorDeclaration(className,
              constructorParameters, Accessibility.Public,
              statements: constructorBody);

            return new SyntaxNode[] {
                dbContextField,
                constructor
            };
        }
        #endregion
    }
}
